﻿using KriniteAuth.ResourceDataApp.Models;

namespace KriniteAuth.ResourceDataApp.ApiServices;

public interface IComplaintService
{
	Task<IEnumerable<ComplaintModel>> GetAllAsync();
	Task<ComplaintModel> GetByIdAsync(Guid id);
	Task<Guid> AddAsync(ComplaintModel complaintModel);
	Task UpdateAsync(ComplaintModel complaintModel);
	Task<int> DeleteAsync(Guid id);
}
