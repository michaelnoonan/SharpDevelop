//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------


using System;
using System.Reflection;
using System.Text;
using SharpReportCore;


/// <summary>
/// This Factory returns BaseItems, this are BaseClasses implementing
/// the <see cref="IItemRenderer"></see>Interface
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 03.09.2005 15:00:17
/// </remarks>

namespace SharpReportCore {
	public class BaseItemFactory : SharpReportCore.GenericFactory {
		
		
		public BaseItemFactory():base(Assembly.GetExecutingAssembly(),
		                              typeof(BaseReportItem)){
		}
		public new BaseReportItem Create (string name){
			if (String.IsNullOrEmpty(name)) {
				throw new ArgumentNullException("name");
			}
			if (name.LastIndexOf('.') > 0) {
				StringBuilder b = new StringBuilder (name);
				b.Remove (0,name.LastIndexOf('.') +1);
				name = b.ToString();
			}	
			return (BaseReportItem) base.Create (name);
			
		}
	}
}
