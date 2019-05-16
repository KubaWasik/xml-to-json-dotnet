using System.Collections.Generic;

namespace XMLtoJSON
{
    public class Field
    {
        public string FieldName { get; set; }
        public string Type { get; set; }
        public dynamic Value { get; set; }
    }

    public class Object
    {
        public string ObjName { get; set; }
        public List<Field> Fields { get; set; }
    }
}
