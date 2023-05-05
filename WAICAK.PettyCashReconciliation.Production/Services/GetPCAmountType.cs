using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAICAK.PettyCashReconciliation.Production.Services
{
    public class GetPCAmountType
    {
        public AccountSummation GetAccountTotals(Vault vault, DateTime startdate, DateTime enddate)
        {
            List<double> mpesatotals = new List<double>();
            List<double> vehiclemaintenancetotals = new List<double>();
            List<double> cateringtotals = new List<double>();
            List<double> businesspromotiontotals = new List<double>();
            List<double> toiletriestotals = new List<double>();
            List<double> mealstotals = new List<double>();
            List<double> localtraveltotals = new List<double>();
            List<double> miscellaneoustotals = new List<double>();
            Lookups oLookups = new Lookups();
            Lookup oLookup = new Lookup();

            AccountSummation summationresponce = new AccountSummation();
            // Create our search builder.
            var searchbuilder = new MFSearchBuilder(vault);
            // Add an object type filter.
            searchbuilder.ObjType(105);
            // Add a "not-deleted" filter.
            searchbuilder.Deleted(false);
            //class
            searchbuilder.Class(138);
            //state
            searchbuilder.Property(39, MFDataType.MFDatatypeLookup, 566);
            // dates range
            searchbuilder.Conditions.BetweenDates(1466, startdate, enddate);
            // Execute the search.
            var searchResults = searchbuilder.FindEx();
            if (searchResults.Count == 0)
            {
                throw new ArgumentNullException("There are no entries during this period");
            }
            else
            {
                SysUtils.ReportInfoToEventLog($"Number of results found is: {searchResults.Count}");
                foreach (ObjVerEx ac in searchResults)
                {
                    oLookup.Item = ac.ObjVer.ID;
                    oLookups.Add(-1, oLookup);
                    var pettycashproperties = vault.ObjectPropertyOperations.GetProperties(ac.ObjVer, false);
                    int actype = Int32.Parse(pettycashproperties.SearchForProperty(1449).TypedValue.GetValueAsLookup().DisplayID);
                    switch (actype)
                    {
                        case 1:
                            mpesatotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 2:
                            vehiclemaintenancetotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 3:
                            cateringtotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 4:
                            businesspromotiontotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 5:
                            toiletriestotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 6:
                            mealstotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        case 7:
                            localtraveltotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                        default:
                            miscellaneoustotals.Add(Double.Parse(pettycashproperties.SearchForProperty(1455).TypedValue.DisplayValue));
                            break;
                    }
                }

                summationresponce.MpesaCharges = mpesatotals.AsQueryable().Sum();
                summationresponce.VehicleMaintenance = vehiclemaintenancetotals.AsQueryable().Sum();
                summationresponce.Catering = cateringtotals.AsQueryable().Sum();
                summationresponce.Businesspromotion = businesspromotiontotals.AsQueryable().Sum();
                summationresponce.ToiletriesDetergents = toiletriestotals.AsQueryable().Sum();
                summationresponce.MealsTransport = mealstotals.AsQueryable().Sum();
                summationresponce.Localtravel = localtraveltotals.AsQueryable().Sum();
                summationresponce.Miscelleneous = miscellaneoustotals.AsQueryable().Sum();
                summationresponce.lookups = oLookups;
                return summationresponce;
            }
        }
    }

}
