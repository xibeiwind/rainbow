﻿using System.Collections.Generic;
using TypeLite.TsModels;

namespace TypeLite.AlternateGenerators
{
    /// <summary>
    ///     Generator implementation that emits classes which extend Backbone.Model
    /// </summary>
    public class TsBackboneModelGenerator : TsGenerator
    {
        protected override void AppendClassDefinition(TsClass classModel, ScriptBuilder sb,
            TsGeneratorOutput generatorOutput)
        {
            string typeName = GetTypeName(classModel);
            string visibility = GetTypeVisibility(classModel, typeName) ? "export " : "";
            sb.AppendFormatIndented(
                "{0}class {1} extends {2}",
                visibility,
                typeName,
                //all bottom-level classes must extend Backbone.Model.
                classModel.BaseType != null ? GetFullyQualifiedTypeName(classModel.BaseType) : "Backbone.Model");

            sb.AppendLine(" {");

            var members = new List<TsProperty>();
            if ((generatorOutput & TsGeneratorOutput.Properties) == TsGeneratorOutput.Properties)
                members.AddRange(classModel.Properties);
            if ((generatorOutput & TsGeneratorOutput.Fields) == TsGeneratorOutput.Fields)
                members.AddRange(classModel.Fields);
            using (sb.IncreaseIndentation())
            {
                foreach (var property in members)
                {
                    if (property.IsIgnored) continue;

                    sb.AppendLineIndented(string.Format(
                        "get {0}(): {1} {{ return this.get(\"{0}\"); }}",
                        GetPropertyName(property), GetPropertyType(property)));

                    sb.AppendLineIndented(string.Format(
                        "set {0}(v: {1}) {{ this.set(\"{0}\", v); }}",
                        GetPropertyName(property), GetPropertyType(property)));
                }
            }

            sb.AppendLineIndented("}");

            _generatedClasses.Add(classModel);
        }
    }
}