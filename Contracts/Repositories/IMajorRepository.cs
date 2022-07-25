﻿using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorRepository
    {
        Task<Pagination<MajorResult>> GetAll(PagingParameters param);
        Task<List<MajorForFilter>> GetAll();
    }
}