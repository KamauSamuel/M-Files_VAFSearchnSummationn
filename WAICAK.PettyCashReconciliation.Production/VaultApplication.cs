using System;
using System.Diagnostics;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using WAICAK.PettyCashReconciliation.Production.Services;

namespace WAICAK.PettyCashReconciliation.Production
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public class VaultApplication
		: VaultApplicationBase
	{
        [StateAction("WFS.GenerateSummary")]
        public void WorkflowStateAction(StateEnvironment env)
        {
            DateTime stdate = DateTime.Parse(env.PropertyValues.SearchForProperty(1057).TypedValue.DisplayValue);
            DateTime enddate = DateTime.Parse(env.PropertyValues.SearchForProperty(1058).TypedValue.DisplayValue);
            GetPCAmountType actotals = new GetPCAmountType();
            var pettycashtotals = actotals.GetAccountTotals(env.Vault, stdate, enddate);
            PropertyValue propmne = new PropertyValue();
            propmne.PropertyDef = 1741;
            PropertyValue proploctravel = new PropertyValue();
            proploctravel.PropertyDef = 1742;
            PropertyValue propvehiclemaintenance = new PropertyValue();
            propvehiclemaintenance.PropertyDef = 1737;
            PropertyValue propmpesacharges = new PropertyValue();
            propmpesacharges.PropertyDef = 1736;
            PropertyValue proptnd = new PropertyValue();
            proptnd.PropertyDef = 1740;
            PropertyValue propbusiensspromo = new PropertyValue();
            propbusiensspromo.PropertyDef = 1739;
            PropertyValue propcatering = new PropertyValue();
            propcatering.PropertyDef = 1738;
            PropertyValue propmiscellaneous = new PropertyValue();
            propmiscellaneous.PropertyDef = 1743;
            PropertyValue propptcash = new PropertyValue();
            propptcash.PropertyDef = 1746;
            propmne.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.MealsTransport);
            proploctravel.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.Localtravel);
            propvehiclemaintenance.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.VehicleMaintenance);
            propmpesacharges.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.MpesaCharges);
            proptnd.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.ToiletriesDetergents);
            propbusiensspromo.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.Businesspromotion);
            propcatering.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.Catering);
            propmiscellaneous.TypedValue.SetValue(MFDataType.MFDatatypeFloating, pettycashtotals.Miscelleneous);
            propptcash.TypedValue.SetValue(MFDataType.MFDatatypeMultiSelectLookup, pettycashtotals.lookups);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propmne);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, proploctravel);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propvehiclemaintenance);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propmpesacharges);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, proptnd);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propbusiensspromo);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propcatering);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propmiscellaneous);
            env.Vault.ObjectPropertyOperations.SetProperty(env.ObjVer, propptcash);
        }
    }
}