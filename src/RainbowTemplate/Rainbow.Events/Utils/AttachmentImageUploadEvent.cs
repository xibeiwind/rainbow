using System;
using System.ComponentModel.DataAnnotations;

using Yunyong.EventBus.Events;

namespace Rainbow.Events.Utils
{
    public class AttachmentImageUploadEvent : EventBase
    {
        public AttachmentImageUploadEvent()
        {

        }
        public Guid ImageId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        /// <summary>
        ///     强制上传
        /// </summary>
        [Display(Name = "强制上传")]
        public bool ForceUpload { get; set; }
    }
}
