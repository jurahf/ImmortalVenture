//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmortalVentureService.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Врач
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Врач()
        {
            this.МедосмотрСВрачом = new HashSet<МедосмотрСВрачом>();
        }
    
        public int Id { get; set; }
        public string ВнешнийХэш { get; set; }
    
        public virtual Пользователь Пользователь { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<МедосмотрСВрачом> МедосмотрСВрачом { get; set; }
    }
}
