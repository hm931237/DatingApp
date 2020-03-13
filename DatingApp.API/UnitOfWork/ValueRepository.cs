using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.UnitOfWork
{
    public class ValueRepository
    {
        private readonly DataContext _Db;
        public ValueRepository(DataContext Db)
        {
            this._Db = Db;
        }
        public IEnumerable<Value> GetValues(){
            return _Db.Values.ToList();
        }
        public Value GetValue(int id){
            return _Db.Values.SingleOrDefault(Value=>Value.Id==id);
        }
    }
}