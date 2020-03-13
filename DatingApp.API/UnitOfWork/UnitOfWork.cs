using DatingApp.API.Data;

namespace DatingApp.API.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly DataContext _Db;
        private ValueRepository _ValueRepository;
        public UnitOfWork(DataContext Db)
        {
            this._Db = Db;
        }

        public ValueRepository ValueRepository{
            get{
                if(_ValueRepository == null){
                    _ValueRepository= new ValueRepository(_Db);
                    }
                return _ValueRepository;
            }
        }
    }
}