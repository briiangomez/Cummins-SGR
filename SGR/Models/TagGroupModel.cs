namespace SGR.Communicator.Models
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Runtime.CompilerServices;

    public class TagGroupModel
    {
        [CMMRequired("Input group name")]
        public string Name { get; set; }
    }
}

