using System.Collections.Generic;
using System.Linq;
using UploadcareCSharp.Data;

namespace UploadcareCSharp.API
{
	/// <summary>
	/// The resource for project, associated with the connecting account.
	/// </summary>
	public class Project
	{
		private readonly ProjectData _projectData;

		internal Project(ProjectData projectData)
		{
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
	            return _projectData.Collaborators.Count > 0
	                ? new Collaborator(_projectData.Collaborators.First())
	                : null;
	        }
	    }

	    public IList<Collaborator> Collaborators
		{
			get
			{
				var collaborators = new List<Collaborator>(_projectData.Collaborators.Count);
			    collaborators.AddRange(_projectData.Collaborators.Select(collaboratorData => new Collaborator(collaboratorData)));
			    return collaborators;
			}
		}

		public class Collaborator
		{
		    private readonly CollaboratorData _collaboratorData;

			internal Collaborator(CollaboratorData collaboratorData)
			{
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