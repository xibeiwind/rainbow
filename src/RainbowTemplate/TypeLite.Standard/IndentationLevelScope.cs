﻿using System;

namespace TypeLite
{
    /// <summary>
    ///     Represents a scope of indentation level in the ScriptBuilder
    /// </summary>
    public class IndentationLevelScope : IDisposable
    {
        private readonly ScriptBuilder _sb;

        /// <summary>
        ///     Initializes a new instance of the indentationLevel class for the specific ScriptBuilder
        /// </summary>
        /// <param name="sb">The Scriptbuilder associated with this Indentation level.</param>
        internal IndentationLevelScope(ScriptBuilder sb)
        {
            _sb = sb;
        }

        /// <summary>
        ///     Disposes the indentation level
        /// </summary>
        public void Dispose()
        {
            _sb.DecreaseIndentation(this);
        }
    }
}