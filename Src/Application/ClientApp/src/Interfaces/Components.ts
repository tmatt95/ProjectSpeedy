/**
 * This is the message which can be displayed at the top of every page.
 */
export interface IGlobalMessage
{
    /**
     * Message text.
     * @type {string}
     */
    message: string;

    /**
     * CSS classes to be added to style the alert
     * @type {string}
     */
    class: string;
}
