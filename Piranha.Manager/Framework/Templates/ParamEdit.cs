﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the param edit view.
	/// </summary>
	[Access(Function="ADMIN_PARAM", RedirectUrl="~/manager/account")]
	public abstract class ParamEdit : Piranha.WebPages.ContentPage<Models.ParamEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
					Model = ParamEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Resources.Settings.EditTitleNewParam ;
					Model = new ParamEditModel() ;
				}
			}
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(ParamEditModel m) {
			Model = m ;
			Page.Title = Piranha.Resources.Settings.EditTitleExistingParam ;

			if (ModelState.IsValid) {
				if (m.Save())
					this.SuccessMessage(Piranha.Resources.Settings.MessageParamSaved) ;
				else this.ErrorMessage(Piranha.Resources.Settings.MessageParamNotSaved) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		public void Delete(string id) {
			Model = ParamEditModel.GetById(new Guid(id)) ;

			if (Model.Delete())
				Response.Redirect("~/manager/param?msg=deleted") ;
			else this.ErrorMessage(Piranha.Resources.Settings.MessageParamNotDeleted) ;
		}
	}
}