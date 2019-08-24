using System.Collections.Generic;
using System.Linq;
using TypeLite.TsModels;

namespace TypeLite
{
    /// <summary>
    ///     Represents script model of CLR classes.
    /// </summary>
    public class TsModel
    {
        /// <summary>
        ///     Initializes a new instance of the TsModel class.
        /// </summary>
        public TsModel()
            : this(new TsClass[] { })
        {
        }

        /// <summary>
        ///     Initializes a new instance of the TsModel class with collection of classes.
        /// </summary>
        /// <param name="classes">The collection of classes to add to the model.</param>
        public TsModel(IEnumerable<TsClass> classes)
        {
            Classes = new HashSet<TsClass>(classes);
            References = new HashSet<string>();
            Modules = new HashSet<TsModule>();
            Enums = new HashSet<TsEnum>();
        }

        /// <summary>
        ///     Initializes a new instance of the TsModel class with collection of classes and enums
        /// </summary>
        /// <param name="classes">The collection of classes to add to the model.</param>
        /// <param name="enums">The collection of enums to add to the model.</param>
        public TsModel(IEnumerable<TsClass> classes, IEnumerable<TsEnum> enums)
        {
            Classes = new HashSet<TsClass>(classes);
            References = new HashSet<string>();
            Modules = new HashSet<TsModule>();
            Enums = new HashSet<TsEnum>(enums);
        }

        /// <summary>
        ///     Gets a collection of classes in the model.
        /// </summary>
        public ISet<TsClass> Classes { get; }

        /// <summary>
        ///     Gets a collection of enums in the model
        /// </summary>
        public ISet<TsEnum> Enums { get; }

        /// <summary>
        ///     Gets a collection of references to other d.ts files.
        /// </summary>
        public ISet<string> References { get; }

        /// <summary>
        ///     Gets a collection of modules in the module.
        /// </summary>
        public ISet<TsModule> Modules { get; }

        /// <summary>
        ///     Runs specific model visitor.
        /// </summary>
        /// <param name="visitor">The model visitor to run.</param>
        public void RunVisitor(ITsModelVisitor visitor)
        {
            visitor.VisitModel(this);

            foreach (var module in Modules) visitor.VisitModule(module);

            foreach (var classModel in Classes)
            {
                visitor.VisitClass(classModel);

                foreach (var property in classModel.Properties.Union(classModel.Fields).Union(classModel.Constants))
                    visitor.VisitProperty(property);
            }

            foreach (var enumModel in Enums) visitor.VisitEnum(enumModel);
        }
    }
}