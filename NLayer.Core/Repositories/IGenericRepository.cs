using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        // GetById async tanımladık çünkü veri tabanında sorgu yapıyor.
        // IQueryable ise veri tabanında direk sorgulamak için kullanmıyoruz orderBy yapabilriz, farklı sorgular üretebilriz. Ondan sonra ToList diyip işlemi gerçekleştiriyoruz.
        // Bu şekilde performansı arttırabiliyoruz.
        // Async birşekilde de tanımlamadık çünkü veri tabanında sorgu yapmadık IQuerayble da.
        Task<T> GetByIdAsync(int id);
        
        IQueryable<T> GetAll(Expression<Func<T,bool>> expression);

        // productRepository.where(x => x.id>5).OrderBy.ToListAsync()
        // ToListAsync() diyene kadar veritabanında sorgu yapmayacak.
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        // Update veya Remove bu ikisinin async methodları yok.
        // Halihazırda memory'de var olan bir nesneyi update ettiğimiz için EFCore state'ini modified olarak değişitiriyor.
        // Bu zaman gerektiren bir işlem değil bundan dolayı async methodları yok bu durum remove içinde geçerli.
        // Asenkron programlamayı neden kullandığımızı hatırlayalım var olan threadleri bloklamamak için işleyisi bozmamak için.
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);




    }
}
