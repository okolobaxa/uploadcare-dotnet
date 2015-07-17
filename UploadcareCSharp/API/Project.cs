using System.Collections.Generic;
using System.Linq;
using Uploadcare.Data;

namespace Uploadcare.API
{
	/// <summary>
	/// The resource for project, associated with the connecting account.
	/// </summary>
	public sealed class Project
	{

		private readonly Client _client;
		private readonly ProjectData _projectData;

		public Project(Client client, ProjectData projectData)
		{
			_client = client;
			_projectData = projectData;
		}

		public string Name
		{
			get
			{
				return _projectData.Name;
			}
		}

		public string PubKey
		{
			get
			{
				return _projectData.PubKey;
			}
		}

		public Collaborator Owner
		{
			get
			{
				if (_projectData.Collaborators.Count > 0)
				{
					return new Collaborator(this, this, _projectData.Collaborators.First());
				}
				else
				{
					return null;
				}
			}
		}

		public IList<Collaborator> Collaborators
		{
			get
			{
				var collaborators = new List<Collaborator>(_projectData.Collaborators.Count);
			    collaborators.AddRange(_projectData.Collaborators.Select(collaboratorData => new Collaborator(this, this, collaboratorData)));
			    return collaborators;
			}
		}

		public sealed class Collaborator
		{
			private readonly Project _outerInstance;
		    private readonly Project _project;
		    private readonly CollaboratorData _collaboratorData;

			internal Collaborator(Project outerInstance, Project project, CollaboratorData collaboratorData)
			{
				_outerInstance = outerInstance;
				_project = project;
				_collaboratorData = collaboratorData;
			}

			public string Name
			{
				get
				{
					return _collaboratorData.Name;
				}
			}

			public string Email
			{
				get
				{
					return _collaboratorData.Email;
				}
			}
		}
	}

}