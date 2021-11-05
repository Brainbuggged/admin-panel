using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Extensions
{
	public class StatusSystemExtension
	{
		public List<StatusLvl> _statusSystem { get; set; } = new List<StatusLvl>();

		public StatusSystemExtension()
		{
			MakeSystem();
		}

		private void MakeSystem()
		{
			{
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.ojidaetsyaOtmena}
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.ojidaetsyaOtpravka }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.ojidaetsyaOtpravka,
					NewStatuses = new List<OrderStatus> { OrderStatus.chastichnayaOtgruzka, OrderStatus.vPuti}
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.ojidaetsyaOtpravka,
					NewStatuses = new List<OrderStatus> { OrderStatus.otkritieSpora}
				});

				//_statusSystem.Add(new StatusLvl
				//{
				//	Delivery = DeliveryType.DostavkaProdavtsom,
				//	Role = RoleType,
				//	Status = OrderStatus.ojidaetsyaOtmena,
				//	NewStatuses = new List<OrderStatus> { OrderStatus, OrderStatus, OrderStatus, }
				//});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.chastichnayaOtgruzka,
					NewStatuses = new List<OrderStatus> { OrderStatus.chastichnayaOtgruzka, OrderStatus.vPuti}
				}); 
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.chastichnayaOtgruzka,
					NewStatuses = new List<OrderStatus> { OrderStatus.otkritieSpora}
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.vPuti,
					NewStatuses = new List<OrderStatus> { OrderStatus.poluchenPokupatelem, OrderStatus.otkritieSpora}
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.poluchenPokupatelem,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon, OrderStatus.otkritieSpora}
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.otkritieSpora,
					NewStatuses = new List<OrderStatus> { OrderStatus.vozvrat, OrderStatus.otkloneniyeVozvrata}
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.otkloneniyeVozvrata,
					NewStatuses = new List<OrderStatus> { OrderStatus.apellyatsiya}
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Seller,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaProdavtsom,
					Role = RoleType.Client,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
			}
			{
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.ojidaetsyaOtmena }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.ojidaetsyaOtpravka }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.ojidaetsyaOtpravka,
					NewStatuses = new List<OrderStatus> { OrderStatus.chastichnayaOtgruzka, OrderStatus.vPuti }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.ojidaetsyaOtpravka,
					NewStatuses = new List<OrderStatus> { OrderStatus.otkritieSpora }
				});

				//_statusSystem.Add(new StatusLvl
				//{
				//	Delivery = DeliveryType.DostavkaPlatformi,
				//	Role = RoleType,
				//	Status = OrderStatus.ojidaetsyaOtmena,
				//	NewStatuses = new List<OrderStatus> { OrderStatus, OrderStatus, OrderStatus, }
				//});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.chastichnayaOtgruzka,
					NewStatuses = new List<OrderStatus> { OrderStatus.chastichnayaOtgruzka, OrderStatus.vPuti }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.chastichnayaOtgruzka,
					NewStatuses = new List<OrderStatus> { OrderStatus.otkritieSpora }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.vPuti,
					NewStatuses = new List<OrderStatus> { OrderStatus.poluchenPokupatelem, OrderStatus.otkritieSpora }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.poluchenPokupatelem,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon, OrderStatus.otkritieSpora }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.otkritieSpora,
					NewStatuses = new List<OrderStatus> { OrderStatus.vozvrat, OrderStatus.otkloneniyeVozvrata }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.otkloneniyeVozvrata,
					NewStatuses = new List<OrderStatus> { OrderStatus.apellyatsiya }
				}); 

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Seller,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.DostavkaPlatformi,
					Role = RoleType.Client,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
			}
			{
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Client,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.ojidaetsyaOtmena }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Seller,
					Status = OrderStatus.vObrabotke,
					NewStatuses = new List<OrderStatus> { OrderStatus.gotovKOtpravke }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Seller,
					Status = OrderStatus.gotovKOtpravke,
					NewStatuses = new List<OrderStatus> { OrderStatus.zakazZabran, OrderStatus.otkritieSpora }
				});

				//_statusSystem.Add(new StatusLvl
				//{
				//	Delivery = DeliveryType.Samovivoz,
				//	Role = RoleType,
				//	Status = OrderStatus.ojidaetsyaOtmena,
				//	NewStatuses = new List<OrderStatus> { OrderStatus, OrderStatus, OrderStatus, }
				//});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Client,
					Status = OrderStatus.zakazZabran,
					NewStatuses = new List<OrderStatus> { OrderStatus.otkritieSpora, OrderStatus.zavershon }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Seller,
					Status = OrderStatus.otkritieSpora,
					NewStatuses = new List<OrderStatus> { OrderStatus.vozvrat, OrderStatus.otkloneniyeVozvrata }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Client,
					Status = OrderStatus.otkloneniyeVozvrata,
					NewStatuses = new List<OrderStatus> { OrderStatus.apellyatsiya }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Seller,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				}); 
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Client,
					Status = OrderStatus.zavershon,
					NewStatuses = new List<OrderStatus> { OrderStatus.archive }
				});

				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Seller,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
				_statusSystem.Add(new StatusLvl
				{
					Delivery = DeliveryType.Samovivoz,
					Role = RoleType.Client,
					Status = OrderStatus.archive,
					NewStatuses = new List<OrderStatus> { OrderStatus.zavershon }
				});
			}

		}
	}

	public class StatusLvl
	{
		public RoleType Role { get; set; }
		public DeliveryType Delivery { get; set; }
		public OrderStatus Status { get; set; }
		public List<OrderStatus> NewStatuses { get; set; }
	}
}
