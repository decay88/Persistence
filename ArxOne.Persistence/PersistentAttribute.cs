﻿#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion

namespace ArxOne.Persistence
{
    using System;
    using MrAdvice.Advice;

    /// <summary>
    /// Persistent properties are saved directly as preferences
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PersistentAttribute : Attribute, IPropertyAdvice
    {
        /// <summary>
        /// Gets or sets the name under which the property is serialized.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this property is automatically saved as soon as it changes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic save]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoSave { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public object DefaultValue { get; set; }

        public PersistentAttribute(string name)
        {
            Name = name;
        }

        public void Advise(PropertyAdviceContext context)
        {
            var persistenceSerializer = Configuration.GetSerializer(context.TargetProperty);
            var persistenceData = Configuration.GetData(context.TargetProperty);
            if (context.IsGetter)
                context.ReturnValue = persistenceData.GetValue(Name, context.TargetProperty.PropertyType, DefaultValue, persistenceSerializer);
            else
                persistenceData.SetValue(Name, context.Value, context.TargetProperty.PropertyType, AutoSave, persistenceSerializer);
        }
    }
}
