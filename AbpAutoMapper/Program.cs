using Abp.AutoMapper;
using Abp.Modules;
using AutoMapper;
using System;

namespace AbpAutoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var order = new Order()
            {
                OrderName = "张三",
                PhoneNumber = "13012345678"
            };
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());
            var mapper = config.CreateMapper();
            OrderDto dto = mapper.Map<OrderDto>(order);

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());
            var mapper2 = new Mapper(config);
            OrderDto dto2 = mapper2.Map<OrderDto>(order);

        }
    }

    public class Order
    {
        public string OrderName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class OrderDto
    {
        public string OrderName { get; set; }
    }

    /// <summary>
    /// 在ABP中灵活使用AutoMapper
    /// https://blog.csdn.net/u014654707/article/details/97938803
    /// https://aspnetboilerplate.com/Pages/Documents/Object-To-Object-Mapping
    /// </summary>
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class MyJobCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Order, AbpOrderDto>()
                //.ForMember(u => u.PhoneNumber, options => options.Ignore())
                .ForMember(u => u.Tel, options => options.MapFrom(input => input.PhoneNumber));
            });
        }
    }

    [AutoMapFrom(typeof(Order))]
    public class AbpOrderDto
    {
        public string OrderName { get; set; }
        public string Tel { get; set; }
    }
}
