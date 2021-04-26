using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DrawingExample.Core
{
    public class Delete
    {

        public Delete() { }

        public void DeleteExplosion(Explosion obj)
        {
            obj = null;
        }

        public void DeleteProjectile(Projectiles obj)
        {
            obj = null;
        }

    }
}
