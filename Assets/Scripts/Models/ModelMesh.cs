using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class ModelMesh
    {
        public string MeshId { get; set; }
		public string MeshType { get; set; }
        public Vect3 Position { get; set; }
        public Vect3 Scale { get; set; }
        public bool IsSelected { get; set; }
    }
}
