using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using System.IO;
using System.Collections;

// using ;
// using AjaxControlToolkit.Design;


/// <summary>
/// Summary description for XmlForm
/// </summary>
public class XmlForm
{
    //private string recursionOutput;

    private XmlDocument resTypeXml;  // resource form definition
    
    private XmlDocument recursionXml;  // class scope used during recursion of response
    private XmlElement recursionXmlRoot;  // class scope used during recursion of response
    
    private IXmlFormCallback callbackHandler; // custom handler used to return dynamic values or options

    private Hashtable responses;

	public bool HideHeader = false;
	public bool HideLegend = true;
	public bool ReadOnly = false;
	public string FieldPrefix = "field_";

    public XmlForm(IXmlFormCallback callback)
    {
        this.callbackHandler = callback;
    }


		/// <summary>
	/// Given a reponse xml document, returns a Hashtable with all the responses
	/// as the values.  The key is determined by the attribute parameter.  For 
	/// example this could be "name" "sp_id" or "rei_id"
	/// </summary>
	/// <param name="doc"></param>
	/// <param name="keyAttribute"></param>
	/// <returns></returns>
	public static Hashtable GetTranslatedHashtable(string defXml, string docXml, string keyAttribute)
	{
		XmlDocument def = new XmlDocument();
		def.LoadXml(defXml);

		XmlDocument doc = new XmlDocument();
		doc.LoadXml(docXml);

		return GetTranslatedHashtable(def,doc,keyAttribute);
	}

	/// <summary>
	/// Given a reponse xml document, returns a Hashtable with all the responses
	/// as the values.  The key is determined by the attribute parameter.  For 
	/// example this could be "name" "sp_id" or "rei_id"
	/// </summary>
	/// <param name="def">Request Definition</param>
	/// <param name="doc">Request Responses</param>
	/// <param name="keyAttribute"></param>
	/// <returns>Hashtable</returns>
	public static Hashtable GetTranslatedHashtable(XmlDocument def, XmlDocument doc, string keyAttribute)
	{
		return XmlForm.GetTranslatedHashtable(def, doc, keyAttribute, false);
	}
	
	/// <summary>
	/// Given a reponse xml document, returns a Hashtable with all the responses
	/// as the values.  The key is determined by the attribute parameter.  For 
	/// example this could be "name" "sp_id" or "rei_id"
	/// </summary>
	/// <param name="def">Request Definition</param>
	/// <param name="doc">Request Responses</param>
	/// <param name="keyAttribute"></param>
	/// <param name="includeBlankValues">True if blank fields should be included (ie questions that the customer did not answer)</param>
	/// <returns>Hashtable</returns>
	public static Hashtable GetTranslatedHashtable(XmlDocument def, XmlDocument doc, string keyAttribute, bool includeBlankValues)
	{
		Hashtable ht = new Hashtable();
		XmlNodeList fields = def.GetElementsByTagName("field");

		// first get a Hashtable for the definition.  we'll need this to
		// translate
		foreach (XmlNode field in fields)
		{
			if (!field.Name.EndsWith("_validator"))
			{
				ht.Add(GetAttribute(field, "name"), GetAttribute(field, keyAttribute));
			}
		}


		Hashtable resp = new Hashtable();
		XmlNodeList rfields = doc.GetElementsByTagName("field");

		//int counter = 1;
		foreach (XmlNode field in rfields)
		{
			if (field.Name.Equals("field")) // this blocks out the child attribs and options
			{
				string name = GetAttribute(field, "name");

				// see if our definition has has a key that matches this fieldname
				// if the definition does not contain such a key, then set it to ""
				string key = ht.ContainsKey(name) ? ht[name].ToString() : "";

				if (key.Equals(""))
				{
					// the keyAttribute is not specifed for this field so that means it
					// should not be exported for this particular format
					// key = name + " UNKNOWN"; // +counter++;
				}
				else
				{
					// only include fields that are not blank - or if includeBlankValues was specified
					if (field.InnerText != "" || includeBlankValues)
					{
						if (resp.ContainsKey(key))
						{
							if (key == "NOTES")
							{
								// the notes field for softpro is where we store all the extra stuff
								resp[key] = resp[key].ToString() + ", " + name + "=" + field.InnerText;
							}
							else
							{
								// most likely this is an address field
								resp[key] = resp[key].ToString() + ", " + field.InnerText;
							}
						}
						else
						{
							if (key == "NOTES")
							{
								// the notes field for softpro is where we store all the extra stuff
								resp.Add(key, name + "=" + field.InnerText);
							}
							else
							{
								// just add the new field
								resp.Add(key, field.InnerText);
							}
						}
					}
				}
			}
		}

		return resp;
	}

	/// <summary>
	/// given an xml response, returns a hashtable with the "name"
	/// attribute as the key and the answer as the value
	/// </summary>
	/// <param name="doc"></param>
	/// <returns></returns>
	public static Hashtable GetResponseHashtable(XmlDocument doc)
	{
		Hashtable ht = new Hashtable();
		XmlNodeList fields = doc.GetElementsByTagName("field");

		string buff;
		foreach (XmlNode field in fields)
		{
			if (field.Name.Equals("field"))
			{
				buff = GetAttribute(field, "name");
				if (!buff.EndsWith("_validator"))
				{
					if(!ht.ContainsKey(GetAttribute(field, "name")))
					{
						ht.Add(GetAttribute(field, "name"), field.InnerText);
					}
					else
					{
						ht[GetAttribute(field, "name")] = field.InnerText;
					}
				}
			}
		}

		return ht;
	}

	public static Hashtable GetResponseHashtable(string resXml)
    {
        if (resXml.Equals(""))
        {
            return new Hashtable();
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(resXml);
        return GetResponseHashtable(doc);
    }

	/// <summary>
	/// given an xml response, returns a hashtable with the "label"
	/// attribute as the key and the answer as the value
	/// </summary>
	/// <param name="doc"></param>
	/// <returns></returns>
	public Hashtable GetLabelHashtable(XmlDocument doc)
	{
		Hashtable ht = new Hashtable();
		XmlNodeList fields = doc.GetElementsByTagName("field");

		foreach (XmlNode field in fields)
		{
			ht.Add(GetAttribute(field, "name"), GetAttribute(field, "label", GetAttribute(field, "name")));
		}

		return ht;
	}

	public Hashtable GetLabelHashtable(string resXml)
    {
        if (resXml.Equals(""))
        {
            return new Hashtable();
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(resXml);
		return GetLabelHashtable(doc);
    }



    /// <summary>
    /// Recurses a control containing XmlForm fields and returns a response xml doc
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public XmlDocument GetResponse(Control parent)
    {
        //this.recursionOutput = "RETURN VALUE";
        this.recursionXml = new XmlDocument();
        this.recursionXml.CreateXmlDeclaration("1.0", "utf-8", null);
        this.recursionXmlRoot = this.recursionXml.CreateElement("response");
        this.recursionXml.AppendChild(this.recursionXmlRoot);

        // recurse all the controls looking for fields
        RecurseResponse(parent);
        
        //return this.recursionOutput;
        return this.recursionXml;
    }

    /// <summary>
    /// converts and XML document to a string
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static string XmlToString(XmlDocument doc)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter xw = new XmlTextWriter(sw);
        doc.WriteTo(xw);
        return sw.ToString();
    }

	/// <summary>
	/// returns a field element with the name property set and the value
	/// </summary>
	/// <param name="doc"></param>
	/// <param name="name"></param>
	/// <param name="val"></param>
	/// <returns></returns>
	public static XmlElement GetXmlField(XmlDocument doc, string name, string val)
	{
		XmlElement field = doc.CreateElement("field");
		field.SetAttribute("name", name);
		field.InnerText = val;

		return field;
	}

    /// <summary>
    /// Recurses all controls starting with the parent control
    /// </summary>
    /// <param name="parent"></param>
    private void RecurseResponse(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            if (ctrl.ID != null && ctrl.ID.StartsWith(this.FieldPrefix))
            {
                //this.recursionOutput += ("<div>" + ctrl.ID.Substring(6) + " [" + ctrl.GetType() + "]" + " = " + GetValue(ctrl) + "<div>");

                XmlElement field = this.recursionXml.CreateElement("field");
                field.SetAttribute("name", ctrl.ID.Substring(6));
                field.InnerText = GetValue(ctrl);

				// if null then this is an unrecognized control - most likely a
				// validator or something that we want to ignore
				if (field.InnerText != null && ctrl.ID.EndsWith("_validator") == false)
				{
					this.recursionXmlRoot.AppendChild(field);
				}
            }

            RecurseResponse(ctrl);
        }
    }

    /// <summary>
    /// returns the value of the given control.  multi-select items return a comma-separated value
    /// </summary>
    /// <param name="ctrl"></param>
    /// <returns></returns>
    public string GetValue(Control ctrl)
    {
        string val;
        switch (ctrl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.TextBox":
                TextBox tb = (TextBox)ctrl;
                val = tb.Text;
                break;
            case "System.Web.UI.WebControls.CheckBoxList":
                val = GetListValue(ctrl);
                break;
            case "System.Web.UI.WebControls.RadioButtonList":
                val = GetListValue(ctrl);
                break;
            case "System.Web.UI.WebControls.DropDownList":
                val = GetListValue(ctrl);
                break;
            default:
				// this is a control that we don't know how to process
                // val = "Unknown Control Type: " + ctrl.GetType().ToString();
				val = null;
                break;
        }

        return val;
    }

    /// <summary>
    /// Returns the value of a ListControl as a comma-separated value
    /// </summary>
    /// <param name="ctrl"></param>
    /// <returns></returns>
    public string GetListValue(Control ctrl)
    {
        ListControl lc = (ListControl)ctrl;
        string val = "";
        string delim = "";
        foreach (ListItem item in lc.Items)
        {
            if (item.Selected)
            {
                val += delim + item.Value.Replace(",", "");
                delim = ",";
            }
        }

        return val;
    }

    public Control GetFormFieldControl(string defXml)
    {
        return GetFormFieldControl(defXml, "");
    }

    /// <summary>
    /// Given properly formatted xml, returns a panel control with form controls
    /// </summary>
    /// <param name="defXml">xml structure: object->group->line->field->option</param>
    /// <param name="resXml">xml structure: object->field</param>
    /// <returns></returns>
    public Control GetFormFieldControl(string defXml, string resXml)
    {
		Control triggers = new Control();
        Panel fields = new Panel();
        fields.CssClass = "fields";

        this.resTypeXml = new XmlDocument();
        this.resTypeXml.LoadXml(defXml);

        this.responses = GetResponseHashtable(resXml);
        XmlNodeList groups = this.resTypeXml.GetElementsByTagName("group");

        foreach (XmlNode group in groups)
        {
			string hiddenTag = IsHidden(group) ? "style=\"display:none;\"" : "";
		    // outer shell of the group
			fields.Controls.Add(new LiteralControl("<div id=\"group_" + GetAttribute(group, "name") + "\" class=\"groupshell\" " + hiddenTag + ">"));

			if (!this.HideHeader)
			{
				fields.Controls.Add(new LiteralControl("<div id=\"group_" + GetAttribute(group, "name") + "_header\" class=\"groupheader\">" + GetAttribute(group, "legend", GetAttribute(group, "name")) + "</div>"));
			}
			fields.Controls.Add(new LiteralControl("<fieldset id=\"group_" + GetAttribute(group, "name") + "_fieldset\">"));
			if (!this.HideLegend)
			{
				fields.Controls.Add(new LiteralControl("<legend "+hiddenTag+">" + GetAttribute(group, "legend", GetAttribute(group, "name")) + "</legend>"));
			}

			int lineCount = 0;
            foreach (XmlNode line in group.ChildNodes)
            {
                Panel ln = GetPanel(line,"line");
				lineCount++;
				ln.Attributes.Add("id", GetAttribute(line, "name", "line_"+lineCount.ToString() ));

				if (IsHidden(line))
				{
					ln.Attributes.Add("style","display:none;");
				}

                foreach (XmlNode field in line.ChildNodes)
                {
					ln.Controls.Add(GetInput(field));

					// if a trigger is defined, add it to the triggers control
					string trigger = GetAttribute(field, "trigger");
					if (!trigger.Equals(""))
					{
						triggers.Controls.Add(new LiteralControl("<script>\r\n"+trigger+"\r\n</script>"));
					}

                }
                fields.Controls.Add(ln);
            }
			fields.Controls.Add(new LiteralControl("</fieldset>"));
			fields.Controls.Add(new LiteralControl("</div>")); // outer
			fields.Controls.Add(triggers); // outer
		}

        return fields;
    }

	/// <summary>
	/// returns true if this node should be hidden by default
	/// </summary>
	/// <param name="node"></param>
	/// <returns></returns>
	public bool IsHidden(XmlNode node)
	{
		string h = GetAttribute(node, "hidden");

		if (h.Equals(""))
		{
			return false;
		}
		else if (h.Equals("true"))
		{
			return true;
		}
		else
		{
			// the format of the hidden parameter should be Field=Value
			// if the value of the response matches, then this is NOT hidden... crazy!
			string delim = "=";
			string[] pair = h.Split(delim.ToCharArray());

			string name = pair[0];
			string val = (this.responses.ContainsKey(name)) ? this.responses[name].ToString() : "";

			if (pair.Length < 1 || pair[1] == null)
			{
				pair[1] = "";
			}

			if (val == pair[1])
			{
				// the values are equal.  don't hide this
				return false;
			}
			else
			{
				// the values do not match - ok to hide
				return true;
			}

		}
	}

	/// <summary>
    /// returns a Panel with some standard stuff set on it based on the node attributes
    /// </summary>
    /// <param name="node"></param>
    /// <param name="baseClass"></param>
    /// <returns></returns>
    public Panel GetPanel(XmlNode node, string baseClass)
    {
        return GetPanel(node, baseClass, "");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="baseClass"></param>
    /// <param name="innerHtml"></param>
    /// <returns></returns>
    public Panel GetPanel(XmlNode node, string baseClass, string innerHtml)
    {
        Panel pnl = new Panel();
        string className = GetAttribute(node, "class");
		pnl.CssClass = baseClass + (className != "" ? " " + className : "");

        if (innerHtml != "")
        {
            pnl.Controls.Add(new LiteralControl(innerHtml));
        }

        return pnl;
    }

    /// <summary>
    /// returns a panel containing a label and an input div.  The input div contains an input control
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
	public WebControl GetInput(XmlNode field)
    {
        return GetInput(field, null);
    }

    public ListItem GetOption(XmlNode item, string selected)
    {
		char[] delim = { ',' };
		ArrayList vals = ArrayList.Adapter(selected.Split(delim));

        ListItem li = new ListItem(GetAttribute(item, "label", GetAttribute(item, "value")), GetAttribute(item, "value"));

		// li.Selected = GetAttribute(item, "selected") == "true";
		li.Selected = vals.Contains(li.Value);

		// add attributes if specified
		foreach (XmlNode attr in item.ChildNodes)
		{
			if (attr.Name == "attribute")
			{
				li.Attributes.Add(GetAttribute(attr, "name"), attr.InnerText);
			}
		}

        return li;
    }
    
    /// <summary>
    /// returns a panel containing a label and an input div.  The input div contains an input control
    /// </summary>
    /// <param name="field"></param>
    /// <param name="response"></param>
    /// <returns></returns>
	public WebControl GetInput(XmlNode field, XmlNode responseField)
    {
        Panel outer = GetPanel(field, "field");
        Panel label = GetPanel(field, GetAttribute(field, "labelclass", "label"), GetAttribute(field, "label", GetAttribute(field, "name") ));
        Panel input = GetPanel(field, GetAttribute(field, "inputclass", "input"));

		outer.Attributes.Add("id", "outer_" + GetAttribute(field, "name"));
		if (IsHidden(field))
		{
			outer.Attributes.Add("style", "display:none;");
		}

        string tipText = GetAttribute(field, "tip");
        Panel tip = GetPanel(field, GetAttribute(field, "tipclass", "tip"), tipText );

        WebControl ctrl;
        WebControl ctrlAttr;
        string fieldName = GetAttribute(field, "name");
		string fieldId = this.FieldPrefix + fieldName;
        string itype = this.ReadOnly ? "readonly" : GetAttribute(field, "input", "text");

        // all the wonderful ways we get the value
        string value = null;
        if (this.responses.ContainsKey(fieldName))
        {
            // form was already filled out
            value = this.responses[fieldName].ToString();
        } 
        else if (GetAttribute(field, "defaultcallback") != "")
        {
            // field specifies a callback handler
            value = this.callbackHandler.GetFormDefault(field, GetAttribute(field, "defaultcallback"));
        }
        else if (value == null)
        {
            // just go with the default, or else empty string
            value = GetAttribute(field, "default");
        }

        // if we have an optioncallback, we need to dynamically add some options
        if (GetAttribute(field, "optioncallback") != "")
        {

            Hashtable ht = this.callbackHandler.GetFormOptions(field, GetAttribute(field, "optioncallback"));

            foreach (string key in ht.Keys)
            {
                XmlElement elem = this.resTypeXml.CreateElement("option");
                elem.SetAttribute("value",key);
                elem.SetAttribute("label",ht[key].ToString());
                field.AppendChild(elem);
            }
        }



		//TODO: clean up to deal with controls more generically and get rid of redundant code

		// base on the "type" attribute, add the correct web control.
        switch (itype)
        {
            case "select":
                DropDownList ddl = new DropDownList();
                ddl.CssClass = "select";
                foreach (XmlNode option in field.ChildNodes)
                {
					if (option.Name == "option")
					{
						ddl.Items.Add(GetOption(option, value));
					}
                }
                ctrl = ddl;
                ctrl.ID = fieldId;
                ctrlAttr = ddl;
                break;
            case "checkbox":
                CheckBoxList cbl = new CheckBoxList();
                cbl.CssClass = "checkboxlist";
                cbl.RepeatDirection = GetAttribute(field, "repeat") == "horizontal" ? RepeatDirection.Horizontal : RepeatDirection.Vertical;
                foreach (XmlNode option in field.ChildNodes)
                {
					if (option.Name == "option")
					{
						cbl.Items.Add(GetOption(option, value));
					}
                }
                ctrl = cbl;
                ctrl.ID = fieldId;
                ctrlAttr = cbl;
               break;
            case "radio":
                RadioButtonList rbl = new RadioButtonList();
                rbl.CssClass = "radiobuttonlist";
                rbl.RepeatDirection = GetAttribute(field, "repeat") == "horizontal" ? RepeatDirection.Horizontal : RepeatDirection.Vertical;
                foreach (XmlNode option in field.ChildNodes)
                {
					if (option.Name == "option")
					{
						rbl.Items.Add(GetOption(option, value));
					}
                }
                ctrl = rbl;
                ctrl.ID = fieldId;
                ctrlAttr = rbl;
                break;
			case "autocomplete":

				Panel pnl2 = new Panel();
				pnl2.CssClass = "autocomplete";

				TextBox atb = new TextBox();
				atb.Text = value;
				atb.Attributes.Add("autocomplete", "off");  // don't want browser autocomplete
				atb.ID = fieldId; // tb
				atb.CssClass = "textbox autocomplete";
				atb.Width = Unit.Pixel(int.Parse(GetAttribute(field, "width", "200")));

				AjaxControlToolkit.AutoCompleteExtender ae = new AjaxControlToolkit.AutoCompleteExtender();
				ae.ID = fieldId + "_ac";
				ae.TargetControlID = fieldId;
				ae.ServiceMethod = GetAttribute(field, "servicemethod");
				ae.ServicePath = GetAttribute(field, "servicepath","AutoComplete.asmx") ;
				ae.MinimumPrefixLength = int.Parse(GetAttribute(field, "prefixlength", "2"));
				ae.CompletionInterval = 500;
				ae.EnableCaching = true;
				ae.CompletionSetCount = int.Parse(GetAttribute(field, "setcount", "12"));

				pnl2.Controls.Add(atb);
				pnl2.Controls.Add(ae);

				ctrl = pnl2;
                ctrlAttr = atb;

				break;
			case "date":

				Panel pnl = new Panel();
				pnl.CssClass = "datepicker";

				TextBox db = new TextBox();
				db.Text = value;
				db.ID = fieldId; // tb
				db.CssClass = "textbox date";
				db.Width = Unit.Pixel(100);

				Image img = new Image();
				img.ImageUrl = "images/ico_calendar.gif";
				img.ID = "img_" + fieldId + "_btn";
				img.CssClass = "calendar_button";

				AjaxControlToolkit.CalendarExtender ce = new AjaxControlToolkit.CalendarExtender();
				ce.TargetControlID = fieldId;
				ce.PopupButtonID = img.ID;
				ce.Format = GetAttribute(field, "format", "MM/dd/yyyy");
				ce.Animated = true;

				//ctrl = db;

				pnl.Controls.Add(db);
				pnl.Controls.Add(img);
				pnl.Controls.Add(ce);

				ctrl = pnl;
                ctrlAttr = db;

				break;
			case "readonly":
				Label lbl = new Label();
				lbl.Text = value;
				lbl.CssClass = "readonly";
				ctrl = lbl;
				ctrl.ID = fieldId;
                ctrlAttr = lbl;
				break;
			default:
                TextBox tb = new TextBox();
                tb.CssClass = "textbox text";
                if (itype.Equals("textarea"))
                {
                    tb.Height = Unit.Pixel(int.Parse(GetAttribute(field, "height", "100")));
                    tb.TextMode = TextBoxMode.MultiLine;
                }
                tb.Width = Unit.Pixel( int.Parse( GetAttribute(field, "width", "200")) );

                tb.Text = value;
                ctrl = tb;
                ctrl.ID = fieldId;
                ctrlAttr = tb;
                break;
        }

        input.Controls.Add(ctrl);
        outer.Controls.Add(label);
        outer.Controls.Add(input);

		// see if a validator is required
		if (GetAttribute(field, "validator", "") == "required")
		{
			RequiredFieldValidator rqv = new RequiredFieldValidator();
			rqv.ID = ctrl.ID + "_validator";
			rqv.ControlToValidate = fieldId;
			rqv.ErrorMessage = fieldId.Replace("field_","") +" is required.";
			rqv.SetFocusOnError = true;
			input.Controls.Add(rqv);
		}

		// add attributes if specified
		foreach (XmlNode attr in field.ChildNodes)
		{
			if (attr.Name == "attribute")
			{
				ctrlAttr.Attributes.Add(GetAttribute(attr, "name"), attr.InnerText);
			}
		}

        if (tipText != "")
        {
            outer.Controls.Add(tip);
        }

        return outer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static string GetAttribute(XmlNode node, string attribute)
    {
        return GetAttribute(node, attribute, "");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="attribute"></param>
    /// <param name="defaultval"></param>
    /// <returns></returns>
    public static string GetAttribute(XmlNode node, string attribute, string defaultval)
    {
        if (node != null && node.Attributes[attribute] != null)
        {
            defaultval = node.Attributes[attribute].InnerText;
        }

        return defaultval;

    }


}
