export class AppMenuItem{
    name = '';
    permissionName = '';
    icon = '';
    route = '';
    items: AppMenuItem[];
    external: boolean;
    requiresAuthentication: boolean;
    featureDependency: any;
    parameters: {};
    moduleId = "";
    modulePath = "";
    fullUrlPath = "";
    visible: boolean;

    constructor(
        name: string,
        permissionName: string,
        icon: string,
        route: string,
        items?: AppMenuItem[],
        external?: boolean,
        parameters?: Object,
        featureDependency?: any,
        requiresAuthentication?: boolean,
        moduleId? : string,
        modulePath?: string,
        fullUrlPath?:string
    ) {
        this.name = name;
        this.permissionName = permissionName;
        this.icon = icon;
        this.route = route;
        this.external = external;
        this.parameters = parameters;
        this.featureDependency = featureDependency;
        if(moduleId)
            this.moduleId = moduleId;
        if(modulePath)
            this.modulePath = modulePath;
        if(fullUrlPath)
            this.fullUrlPath = fullUrlPath;

        if (items === undefined) {
            this.items = [];
        } else {
            this.items = items;
        }

        if (this.permissionName) {
            this.requiresAuthentication = true;
        } else {
            this.requiresAuthentication = requiresAuthentication ? requiresAuthentication : false;
        }
    }

    hasFeatureDependency(): boolean {
        return this.featureDependency !== undefined;
    }

    featureDependencySatisfied(): boolean {
        if (this.featureDependency) {
            return this.featureDependency();
        }

        return false;
    }
}
