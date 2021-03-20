import { Dispatch } from "react";
import { BreadCrumbItem } from "../Components/BreadCrumbs";
import { IGlobalMessage } from "./Components";

/**
 * This is the interface for the parameters passed to every page.
 */
export interface IPage
{
    /**
     * Lets you update the pages breadcrumbs.
     */
    setBreadCrumbs: Dispatch<BreadCrumbItem[]>;

    /**
     * Lets you get the current page breadcrumbs.
     */
    breadCrumbs: BreadCrumbItem[];

    /**
     * Function you can call to display a message at the top of the page.
     */
    globalMessage: (alertMessage: IGlobalMessage) => void;
}
