# XML to JSON converter in C#

program use .Net Core 2.2 App and Newtonsoft.Json package

Valid XML to convert should look like:
root could be any other node like objects
```xml
  <root>
    <object>
      <obj_name> object name </obj_name>
      <field>
        <name> field name </name>
        <type> field type </type>
        <value> field value </value>
      </field>
      
      ... other fields
      
    </object>
    
    ... other objects
    
  </root>
```
  
program will produce output JSON file:
```json
  {
    "object name" : {
      "field name": "field value",
      
      ... other fields
    
    },
    
    ... other objects
    
  }
```
