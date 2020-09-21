namespace SGR.Communicator.Models
{
    using System;

    public interface IContactGroupCampaignModel
    {
        Guid ContactGroupId { get; set; }

        string ContactGroupName { get; set; }

        string Opt { get; set; }
    }
}

