using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Base
{
    public abstract class Entity
    {
        #region Properties

        /// <summary>
        /// Defines creation date for domain entity.
        /// </summary>
        public virtual DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Defines modified date for domain entity.
        /// </summary>
        public virtual DateTime? FechaModificacion { get; set; }

        #endregion
    }
}
