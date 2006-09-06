/*
 * User: dickon
 * Date: 06/09/2006
 * Time: 08:43
 * 
 */

using System;
using System.Data;
using System.Windows.Forms;

using ICSharpCode.SharpDevelop.Gui;
using SharpDbTools.Model;


namespace SharpDbTools.Viewer
{
	/// <summary>
	/// Description of TableDescribeViewContent.
	/// </summary>
	public class TableDescribeViewContent : AbstractViewContent
	{
		DataTable tableInfo;
		DataGridView tableInfoDataGridView;
		
		
		public TableDescribeViewContent(DataTable tableInfo, string tableName) : base("table: " + tableName)
		{		
			this.tableInfo = tableInfo;
			this.tableInfoDataGridView = new DataGridView();
			DataGridView v = this.tableInfoDataGridView;

			v.AutoGenerateColumns = false;
			v.AutoSize = true;
			
			v.DataSource = this.tableInfo;
			//v.DataMember = TableNames.Columns;
			
			string[] fieldsToDisplay = ColumnNames.TableTableFieldsToDisplay;
			string[] fieldColumnNames = ColumnNames.TableTableFieldsColumnHeaders;
			for (int i = 0; i < fieldsToDisplay.Length; i++ ) {
				DataGridViewColumn c = new DataGridViewTextBoxColumn();
				c.DataPropertyName = fieldsToDisplay[i];
				c.Name = fieldColumnNames[i];
				v.Columns.Add(c);
			}
			v.AllowUserToAddRows = false;
			v.AllowUserToDeleteRows = false;
			v.AllowUserToResizeRows = false;
			v.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			v.AutoResizeColumns();
		}
		
		public override System.Windows.Forms.Control Control {
			get {
				return this.tableInfoDataGridView;
			}
		}
		
		public override bool IsReadOnly {
			get {
				return true;
			}
		}
		
		public override bool IsViewOnly {
			get {
				return true;
			}
		}
	}
}
