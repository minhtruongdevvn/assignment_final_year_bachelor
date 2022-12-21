using AutoMapper;

namespace AAM.Application;

public abstract class BaseCrudDTO : BaseDTO
{
    IMapper? _mapper = null;
    protected IMapper mapper
    {
        get
        {
            if (_mapper == null)
            {
                _mapper = GetConfiguration().CreateMapper();
            }
            return _mapper;
        }
    }

    protected abstract MapperConfiguration GetConfiguration();
    protected MapperConfiguration GetGenericConfiguration<TModel, TDto, TUpdate, TAdd>()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TDto, TUpdate>();
            cfg.CreateMap<TDto, TAdd>();
            cfg.CreateMap<TUpdate, TModel>();
            cfg.CreateMap<TAdd, TModel>();
        });
    }
}

