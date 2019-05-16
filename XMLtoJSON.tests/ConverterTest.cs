using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace XMLtoJSON.tests
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            string res = Convert.ConvertFunction("");
            Assert.AreEqual(res, null);
        }

        [TestMethod]
        public void TestValidInput()
        {
            string result = Convert.ConvertFunction(@"<objects>
                    <object>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <type>string</type>
                            <value>str</value>
                        </field>
                    </object>
                </objects>");
            result = new string(result.Where(c => !char.IsWhiteSpace(c)).ToArray());
            string correctResult = @"{
                    ""object name"": {
                    ""int field name"": 1,
                    ""string field name"": ""str""
                }
            }";
            correctResult = new string(correctResult.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.AreEqual(result, correctResult);
        }

        [TestMethod]
        public void TestInvalidObjectName()
        {
            string result = Convert.ConvertFunction(@"<objects>
                    <obiect>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <type>string</type>
                            <value>str</value>
                        </field>
                    </object>
                </objects>");
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void TestInvalidFieldsName()
        {
            string result = Convert.ConvertFunction(@"<objects>
                    <object>
                        <obj_name>object name</obj_name>
                        <lolfield>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </lolfield>
                        <pokemon_field>
                            <name>string field name</name>
                            <type>string</type>
                            <value>str</value>
                        </pokemon_field>
                    </object>
                </objects>");
            Assert.AreEqual(result, "{}");
        }

        [TestMethod]
        public void TestInvalidFieldName()
        {
            string result = Convert.ConvertFunction(@"<objects>
                    <object>
                        <obj_name>object name</obj_name>
                        <pokemon_field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </pokemon_field>
                        <field>
                            <name>string field name</name>
                            <type>string</type>
                            <value>str</value>
                        </field>
                    </object>
                </objects>");
            result = new string(result.Where(c => !char.IsWhiteSpace(c)).ToArray());
            string correctResult = @"{
                    ""object name"": {

                    ""string field name"": ""str""
                }
            }";
            correctResult = new string(correctResult.Where(c => !char.IsWhiteSpace(c)).ToArray());
            Assert.AreEqual(result, correctResult);
        }

        [TestMethod]
        public void TestInvalidFieldTypeName()
        {
            string result = Convert.ConvertFunction(@"<objects>
                    <object>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <typee>int</typee>
                            <value>1</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <typee>string</typee>
                            <value>str</value>
                        </field>
                    </object>
                </objects>");
            Assert.AreEqual(result, "{}");
        }

        [TestMethod]
        public void TestNoRootElement()
        {
            string result = Convert.ConvertFunction(@"
                    <object>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <typee>string</typee>
                            <value>str</value>
                        </field>
                    </object>
                    <object>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>1</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <typee>string</typee>
                            <value>str</value>
                        </field>
                    </object>
                ");
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void TestInvalidFieldType()
        {
            string result = Convert.ConvertFunction(@"
                <objects>
                    <object>
                        <obj_name>object name</obj_name>
                        <field>
                            <name>int field name</name>
                            <type>int</type>
                            <value>nope</value>
                        </field>
                        <field>
                            <name>string field name</name>
                            <type>tuple</type>
                            <value>(str,1)</value>
                        </field>
                    </object>
                </objects>");
            Assert.AreEqual(result, "{}");
        }
    }
}
