using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreZhaoMvc.CacheEntrys;
using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Service.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AspNetCoreZhaoMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IMemoryCache memory;
        private readonly IDistributedCache redisCache;

        public StudentController(IStudentService _studentService, IMemoryCache _memory, IDistributedCache _redisCache)
        {
            studentService = _studentService;
            memory = _memory;
            redisCache = _redisCache;
        }
        /// <summary>
        /// 客户端缓存
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration =30,Location =ResponseCacheLocation.Client)]
        public async Task<IActionResult> Index()
        {
            #region MemoryCache
            //if (!memory.TryGetValue(CacheEntryConstants.AlbumsOfToday,out IEnumerable<Student> students))
            //{
            //    students= studentService.GetAlls();
            //    //设置缓存
            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        //设置固定.SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(30)); //可变的时间
            //    cacheEntryOptions.RegisterPostEvictionCallback(callback,this);

            //}
            #endregion
            #region Redis
            IEnumerable<Student> students = null;
            var cachedstudents = redisCache.Get(CacheEntryConstants.AlbumsOfToday);
            if (cachedstudents==null)
            {
                students = studentService.GetAlls();
                var serializedString = JsonConvert.SerializeObject(students);
                byte[] encodedStudents = Encoding.UTF8.GetBytes(serializedString);
                //设置缓存
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    //设置固定.SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30)); //可变的时间
                redisCache.Set(CacheEntryConstants.AlbumsOfToday, encodedStudents);
            }
            else
            {
                byte[] encodedStudents = redisCache.Get(CacheEntryConstants.AlbumsOfToday);
                var serializedString = Encoding.UTF8.GetString(encodedStudents);
                students = JsonConvert.DeserializeObject<IEnumerable<Student>>(serializedString);
            }
            #endregion
            return View(students);
        }

        private void callback(object key, object value, EvictionReason reason, object state)
        {
            throw new NotImplementedException();
        }
    }
}