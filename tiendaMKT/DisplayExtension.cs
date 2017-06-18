using System;
namespace tiendaMKT
{
	using System;
	using System.ComponentModel;
	
	using Xamarin.Forms.Xaml;

	public class DisplayExtension : IMarkupExtension<string>
	{
		public object Target { get; set; }
		BindableProperty _Property;

		public string ProvideValue(IServiceProvider serviceProvider)
		{
			if (Target == null
			  || !(Target is Enum
				|| Target is Type
				|| (Target is Binding binding && !string.IsNullOrWhiteSpace(binding.Path))))
				throw new InvalidOperationException($"'{nameof(Target)}' must be properly set.");

			var pvt = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

			if (!(pvt.TargetObject is BindableObject element
			  && pvt.TargetProperty is BindableProperty bp
			  && bp.ReturnType.GetTypeInfo().IsAssignableFrom(typeof(string).GetTypeInfo())))
				throw new InvalidOperationException(
				  $"'{nameof(DisplayExtension)}' cannot only be applied to bindable string properties.");

			_Property = bp;

			element.BindingContextChanged += BindingContextChanged;

			var parent = pvt.ProvideParent();// ProvideParent(pvt);
			parent.ChildRemoved += (sender, e) =>
			  {
				  if (e.Element == element)
					  element.BindingContextChanged -= BindingContextChanged;
			  };

			return null;
		}


		void BindingContextChanged(object sender, EventArgs e)
		{
			var element = (BindableObject)sender;

			string display = null;
			if (Target is Binding binding)
				display = ExtractMember(element, (Binding)Target);
			else if (Target is Type type)
				display = ExtractDescription(type.GetTypeInfo());
			else if (Target is Enum en)
			{
				var enumType = en.GetType();
				if (!Enum.IsDefined(enumType, en))
					throw new InvalidOperationException($"The value '{en}' is not defined in '{enumType}'.");
				display = ExtractDescription(enumType.GetTypeInfo().GetDeclaredField(en.ToString()));
			}
			element.SetValue(_Property, display);
		}

		string ExtractMember(BindableObject target, Binding binding)
		{
			var container = target.BindingContext;
			var properties = binding.Path.Split('.');

			var i = 0;

			do
			{
				var property = properties[i++];
				var type = container.GetType();
				var info = type.GetRuntimeProperty(property);

				if (properties.Length > i)
					container = info.GetValue(container);
				else
				{
					if (info == null)
						return property;
					else
						return ExtractDescription(info);
				}
			} while (true);
		}

		string ExtractDescription(MemberInfo info)
		{
			var display = info.GetCustomAttribute<DisplayAttribute>(true);
			if (display != null)
				return display.Name ?? display.Description;

			var description = info.GetCustomAttribute<DescriptionAttribute>(true);
			if (description != null)
				return description.Description;

			return info.Name;
		}

		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
		  ProvideValue(serviceProvider);
	}
}

namespace System.ComponentModel
{
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		public string Description { get; }

		public DescriptionAttribute(string description) =>
		  Description = description ?? throw new ArgumentNullException(nameof(description));
	}
}

namespace Xamarin.Forms.Xaml
{
	using System.Collections.Generic;
	using System.Linq;

	public static class ProvideValueTargetExtensions
	{
		static readonly TypeInfo IProvideParentValues =
		  typeof(IProvideValueTarget).GetTypeInfo().Assembly.DefinedTypes.Single(t => t.Name == "IProvideParentValues");
		static readonly PropertyInfo ParentObjects = IProvideParentValues.GetDeclaredProperty("ParentObjects");

		public static IEnumerable<object> GetParentObjects(this IProvideValueTarget pvt) =>
		  (IEnumerable<object>)ParentObjects.GetValue(pvt);

		public static Element ProvideParent(this IProvideValueTarget pvt)
		{
			var parents = pvt.GetParentObjects();
			var element = parents
			  .SkipWhile(p => p == pvt.TargetObject)
			  .First();
			return (Element)element;
		}
	}
}