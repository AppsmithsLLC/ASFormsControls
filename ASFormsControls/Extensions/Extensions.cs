using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace ASFormsControls.Extensions
{
    public static class Extensions
    {
        private static readonly MethodInfo BindingBaseApplyMethod =
            typeof(BindingBase).GetTypeInfo().GetDeclaredMethods("Apply").Single(mi => mi.GetParameters().Length == 3);

        private static readonly MethodInfo BindingBaseUnapplyMethod =
            typeof(BindingBase).GetTypeInfo().GetDeclaredMethod("Unapply");

        public static void Apply(this BindingBase binding, object context, BindableObject bindObj,
            BindableProperty targetProperty)
        {
            BindingBaseApplyMethod.Invoke(binding, new[] { context, bindObj, targetProperty });
        }

        public static void Unapply(this BindingBase binding)
        {
            BindingBaseUnapplyMethod.Invoke(binding, new object[0]);
        }
    }
}
