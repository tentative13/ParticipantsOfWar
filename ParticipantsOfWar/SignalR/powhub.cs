using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ParticipantsOfWar.BLL;
using ParticipantsOfWar.Models;
using ParticipantsOfWar.Dto;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;


namespace ParticipantsOfWar
{
    public class PowHub : Hub
    {
        private readonly static List<ConnectionsKeeper> _keepers = new List<ConnectionsKeeper>();
        public IArchiveRepository _archiveRepo;

        public PowHub()
        {
            _archiveRepo = new ArchiveRepository();
        }
        public override Task OnConnected()
        {
            if(!_keepers.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                ConnectionsKeeper newkeeper = new ConnectionsKeeper();
                newkeeper.partisipants = new List<Guid>();
                newkeeper.ConnectionId = Context.ConnectionId;
                _keepers.Add(newkeeper);
            }

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            _keepers.RemoveAll(x => x.ConnectionId == Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            if (!_keepers.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                ConnectionsKeeper newkeeper = new ConnectionsKeeper();
                newkeeper.partisipants = new List<Guid>();
                newkeeper.ConnectionId = Context.ConnectionId;
                _keepers.Add(newkeeper);
            }
            return base.OnReconnected();
        }

        public ParticipantsDto[] GetParticipants(TableFilter filter, int number)
        {
            var already_send_guids = _keepers.Where(x => x.ConnectionId == Context.ConnectionId).Select(x => x.partisipants).FirstOrDefault();

            var response = _archiveRepo.GetFiltered(filter, already_send_guids);

            if (response.Count() > 0)
            {
                if (response.Count() > number) response = response.Take(number).ToList();

                int? i = _keepers.IndexOf(_keepers.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault());
                if (i != null) _keepers[i.Value].partisipants.AddRange(response.Select(x => new Guid(x.guid)));
            }
            return response.ToArray();
        }

        public ParticipantsDto[] GetAllParticipants(TableFilter filter)
        {
            var already_send_guids = _keepers.Where(x => x.ConnectionId == Context.ConnectionId).Select(x => x.partisipants).FirstOrDefault();

            var response = _archiveRepo.GetFiltered(filter, already_send_guids);

            if (response.Count() > 0)
            {
                int? i = _keepers.IndexOf(_keepers.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault());
                if (i != null) _keepers[i.Value].partisipants.AddRange(response.Select(x => new Guid(x.guid)));
            }
            return response.ToArray();
        }
        public int GetTotalFilteredParticipants(TableFilter filter)
        {
            return _archiveRepo.GetFilteredTotal(filter);
        }
        public int GetTotalParticipants()
        {
            return _archiveRepo.GetAll().Count();
        }
        public void guidscache(string[] guids)
        {
            int index = _keepers.IndexOf(_keepers.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault());

            foreach(var guid in guids)
            {
                _keepers[index].partisipants.Add(new Guid(guid));
            }

            _keepers[index].partisipants = _keepers[index].partisipants.Distinct<Guid>().ToList();
        }

    }

    public class ConnectionsKeeper
    {
        public string ConnectionId { get; set; }
        public List<Guid> partisipants { get; set; }
    }


}