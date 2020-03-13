using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private DataContext _Db;
        private API.UnitOfWork.UnitOfWork _UnitOfWork;
        public ValuesController(DataContext Db)
        {
            this._Db = Db;
            this._UnitOfWork=new API.UnitOfWork.UnitOfWork(Db);
        }
        [HttpGet]

        public IActionResult GetValues(){
            return Ok(_UnitOfWork.ValueRepository.GetValues());
        }
        // public async Task<IActionResult> GetValues(){
        //     var Values=await _Db.Values.ToListAsync();
        //     return Ok(Values) ;
        //     //return Ok(_UnitOfWork.ValueRepository.GetValues());
        // }

        [HttpGet("{id}")]
        public IActionResult GetValue(int id){
            return Ok(_UnitOfWork.ValueRepository.GetValue(id));
        }
    }
}