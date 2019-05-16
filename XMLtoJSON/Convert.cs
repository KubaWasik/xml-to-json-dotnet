using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XMLtoJSON
{
    public class Convert
    {
        public static string ConvertFunction(string xml)
        {
            // objectList will be result before serialize to json
            List<Object> objectsList = new List<Object>();
            XElement rootElement = null;
            // try parse xml, return null if not valid
            try
            {
                rootElement = XElement.Parse(xml);
            }
            catch (XmlException)
            {
                return (null);
            }
            foreach (XElement obj in rootElement.Elements("object"))
            {
                string objectName = null;
                if (obj.Element("obj_name") != null)
                {
                    string tmpObjName = obj.Element("obj_name").Value.Trim();
                    if (tmpObjName.Length > 0)
                    {
                        objectName = tmpObjName;
                    }
                    else
                    {
                        // Object has empty name
                        continue;
                    }
                }
                else
                {
                    // Object has no name
                    continue;
                }
                List<Field> fieldList = new List<Field>();
                foreach (XElement field in obj.Elements("field"))
                {
                    string fieldName = null, fieldType = null;
                    dynamic fieldValue = null;
                    if (field.Element("name") != null)
                    {
                        string tmpName = field.Element("name").Value.Trim();
                        if (tmpName.Length > 0)
                        {
                            fieldName = tmpName;
                        }
                        else
                        {
                            // Field has empty name
                            continue;
                        }
                    }
                    else
                    {
                        // Field has no name
                        continue;
                    }
                    if (field.Element("type") != null)
                    {
                        string tmpType = field.Element("type").Value.Trim();
                        if (tmpType.Length > 0 && (tmpType == "string" || tmpType == "int"))
                        {
                            fieldType = tmpType;
                        }
                        else
                        {
                            // Field has empty type or field type is not allowed 
                            continue;
                        }
                    }
                    else
                    {
                        // Field has no type
                        continue;
                    }
                    if (field.Element("value") != null)
                    {
                        string tmpValue = field.Element("value").Value.Trim();
                        if (fieldType == "int")
                        {
                            if (int.TryParse(tmpValue, out int tmpValueInt))
                            {
                                fieldValue = tmpValueInt;
                            }
                            else
                            {
                                // Value type and field type not equal
                                continue;
                            }
                        }
                        else if (tmpValue.Length > 0)
                        {
                            fieldValue = tmpValue;
                        }
                        else
                        {
                            // Field has empty value
                            continue;
                        }
                    }
                    else
                    {
                        // Field has no value
                        continue;
                    }
                    // If field is valid create field (class object)
                    if (fieldName != null && fieldType != null && fieldValue != null)
                    {
                        Field classField = new Field
                        {
                            FieldName = fieldName,
                            Type = fieldType,
                            Value = fieldValue
                        };
                        fieldList.Add(classField);
                    }
                    else
                    {
                        // Probably unnecesary but if were some erros in field then skip this field
                        continue;
                    }
                }
                // if exist at least one field create object
                if (fieldList.Count > 0)
                {
                    Object classObject = new Object
                    {
                        ObjName = objectName,
                        Fields = fieldList
                    };
                    objectsList.Add(classObject);
                }
                else
                {
                    // Object has no fields
                    continue;
                }
            }
            JObject objects =
                new JObject(
                    from obj in objectsList
                    select
                        new JProperty(obj.ObjName,
                            new JObject(
                                from field in obj.Fields
                                select new JProperty(field.FieldName, field.Value))));
            return objects.ToString();
        }
    }
}
