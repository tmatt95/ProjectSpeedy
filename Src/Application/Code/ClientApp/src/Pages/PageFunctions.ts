import * as bootstrap from 'bootstrap';
import { MouseEvent } from 'react';
export class PageFunctions
{

    /**
     * Resets the form validators etc.
     */
    static ResetForm()
    {
        let form: HTMLFormElement | null = document.getElementById("new-form") as HTMLFormElement;
        if (form !== null)
        {
            form.reset();
        }
    }

    /**
     * Loads the modal onto the screen.
     */
    static DisplayModal(e: MouseEvent, dialogOpened: boolean, setDialogOpened: (newValue: boolean) => void)
    {
        e.preventDefault();
        let myModalEl: HTMLElement | null = document.getElementById('newModal');
        if (myModalEl !== null)
        {
            if (dialogOpened === false)
            {
                // Resets when dialog open
                myModalEl.addEventListener('show.bs.modal', function (event)
                {
                    PageFunctions.ResetForm();
                });
                setDialogOpened(true);
            }

            // Opens the modal.
            let modal = new bootstrap.Modal(myModalEl, { keyboard: false });
            if (modal != null)
            {
                modal.show();
            }
        }
    }

    /**
     * Close a dialog.
     * @param id The html id of the element to close.
     */
    static CloseDialog(id: string)
    {
        let myModalEl: HTMLElement | null = document.getElementById(id);
        if (myModalEl != null)
        {
            let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl);
            if (modal != null)
            {
                modal.hide();
            }
        }
    }
}