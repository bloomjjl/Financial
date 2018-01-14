using Financial.Core;
using Financial.Core.ViewModels.AssetTypeRelationshipType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeRelationshipTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeRelationshipTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeRelationshipTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int relationshipTypeId)
        {
            // transfer dto for Id
            var dtoRelationshipType = _unitOfWork.RelationshipTypes.Get(relationshipTypeId);

            // get ParentAssetTypes linked to Id
            var dbParentChildRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ParentChildRelationshipTypeId == relationshipTypeId)
                .ToList();

            // transfer Parent to vm
            var vmIndexLinkedAssetTypes = new List<IndexLinkedAssetTypesViewModel>();
            foreach (var dtoParentRelationship in dbParentChildRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoParentRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentRelationship.ParentChildRelationshipTypeId);
                var dtoChildParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoParentRelationship.ChildParentRelationshipTypeId);
                vmIndexLinkedAssetTypes.Add(new IndexLinkedAssetTypesViewModel(dtoParentRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentChildRelationshipType, dtoChildParentRelationshipType));
            }

            // get ChildAssetTypes linked to Id
            var dbChildParentRelationships = _unitOfWork.AssetTypesRelationshipTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.ChildParentRelationshipTypeId == relationshipTypeId)
                .ToList();

            // transfer Child to vm
            foreach (var dtoChildRelationship in dbChildParentRelationships)
            {
                var dtoParentAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ParentAssetTypeId);
                var dtoChildAssetType = _unitOfWork.AssetTypes.Get(dtoChildRelationship.ChildAssetTypeId);
                var dtoParentChildRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoChildRelationship.ParentChildRelationshipTypeId);
                var dtoChildParentRelationshipType = _unitOfWork.RelationshipTypes.Get(dtoChildRelationship.ChildParentRelationshipTypeId);
                vmIndexLinkedAssetTypes.Add(new IndexLinkedAssetTypesViewModel(dtoChildRelationship, dtoParentAssetType, dtoChildAssetType, dtoParentChildRelationshipType, dtoChildParentRelationshipType));
            }

            // display view
            return PartialView("_IndexLinkedAssetTypes", vmIndexLinkedAssetTypes);
        }
    }
}