using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     ModelTypeMeta
    /// </summary>
    [Display(Name = "ModelTypeMeta")]
    [Table(nameof(ModelTypeMeta))]
    public class ModelTypeMeta : Entity
    {
        public string TypeName { get; set; }
        public string JSON { get; set; }
    }
}
