﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorBase.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   https://github.com/davidbritch/xamarin-forms/blob/master/ItemSelectedBehavior/ItemSelectedBehavior/Behaviors/BehaviorBase.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// This file isn't generated, but this comment is necessary to exclude it from StyleCop analysis.
// <auto-generated/>

using System.ComponentModel;

namespace Mvvm.Flux.Maui.Presentation.Behaviors
{
    public class BehaviorBase<T> : Behavior<T>
        where T : BindableObject
    {
        public BehaviorBase()
        {
            BindToTargetContext = true;
        }

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        public T AssociatedObject { get; private set; }

        public bool BindToTargetContext { get; set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.PropertyChanged += OnAssociatedObjectPropertyChanged;
        }

        protected virtual void OnAssociatedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.PropertyChanged -= OnAssociatedObjectPropertyChanged;
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindToTargetContext && AssociatedObject != null)
            {
                BindingContext = AssociatedObject.BindingContext;
            }
        }
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}