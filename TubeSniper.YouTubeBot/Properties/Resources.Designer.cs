﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TubeSniper.YouTubeBot.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TubeSniper.YouTubeBot.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*! jQuery v3.3.1 | (c) JS Foundation and other contributors | jquery.org/license */
        ///!function(e,t){&quot;use strict&quot;;&quot;object&quot;==typeof module&amp;&amp;&quot;object&quot;==typeof module.exports?module.exports=e.document?t(e,!0):function(e){if(!e.document)throw new Error(&quot;jQuery requires a window with a document&quot;);return t(e)}:t(e)}(&quot;undefined&quot;!=typeof window?window:this,function(e,t){&quot;use strict&quot;;var n=[],r=e.document,i=Object.getPrototypeOf,o=n.slice,a=n.concat,s=n.push,u=n.indexOf,l={},c=l.toString,f=l.hasOwnProperty,p=f.toStrin [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string jquery_min {
            get {
                return ResourceManager.GetString("jquery_min", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (function () {
        ///    var videos = [];
        ///    var items = $(&quot;ytd-video-renderer&quot;);
        ///    items.each(function (index, item) {
        ///        var video = {};
        ///        video[&quot;id&quot;] = $(&quot;#video-title&quot;, item).attr(&quot;href&quot;);
        ///        video[&quot;title&quot;] = $(&quot;#video-title&quot;, item).attr(&quot;title&quot;);
        ///        video[&quot;chanel_name&quot;] = $(&quot;a[href*=\&quot;user\&quot;]&quot;, item).text();
        ///        video[&quot;channel_url&quot;] = $(&quot;a[href*=\&quot;user\&quot;]&quot;, item).attr(&quot;href&quot;);
        ///        video[&quot;views&quot;] = $(&quot;#metadata-line &gt; span:nth-child(1)&quot;, item).text();
        ///        video[&quot;d [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string video_extraction {
            get {
                return ResourceManager.GetString("video_extraction", resourceCulture);
            }
        }
    }
}