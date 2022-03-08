using System;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.Navigations
{
    public class NavigationDto
    {
        public string Name { get; set; }
        public string PermissionName { get; set; }
        public string DisplayNamePtBr { get; set; }
        public string DisplayNameEnUs { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public List<NavigationDto> Items { get; set; }
        public bool? External { get; set; }
        public Guid? ModuleId { get; set; }
        public string ModulePath { get; set; }
        public string FullUrlPath { get; set; }
        public bool Visible { get; set; }
        public long Id { get; set; }
        public string Rota { get; set; }
        public string ModuloAcesso { get; set; }
	}
}