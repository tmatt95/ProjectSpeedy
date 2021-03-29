import { useState, useEffect, Dispatch, SetStateAction, MouseEvent } from 'react';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage } from '../Interfaces/IPage';
import { ProjectService } from '../Services/ProjectService'
import ProjectNewForm from '../Components/Projects/ProjectNewForm'
import * as bootstrap from 'bootstrap';

export function Projects(pageProps: IPage) 
{
  /**
   * Existing projects.
   */
  const [projects, setProjects]: [CardItem[], Dispatch<SetStateAction<CardItem[]>>] = useState(new Array<CardItem>());

  /**
   * Used to run code only once on page load.
   */
  const [runOnce, setRunOnce] = useState(false);
  useEffect(() =>
  {
    if (runOnce === false)
    {
      document.title = 'Projects';
      pageProps.setBreadCrumbs([{ text: "Projects", address: "/", isLast: true }]);
      setRunOnce(true);

      // Loads the projects onto the page
      ProjectService.GetAll().then(
        (data) =>
        {
          setProjects(data);
        },
        (error) =>
        {
          alert(error);
        }
      );
    }
  }, [runOnce, pageProps]);

  /**
   * Whether the dialog has even been opened.
   */
  const [dialogOpened, setDialogOpened] = useState(false);

/**
     * Resets the form validators etc.
     */
 function ResetForm()
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
  function DisplayModal(e: MouseEvent)
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
                 ResetForm();
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

  return (<>
    <div className="row">
      <div className="col">
        <h1>Projects</h1>
      </div>
    </div>
    <CardGrid data={projects} AddNewClick={(e) => {DisplayModal(e)}} />
    <ProjectNewForm setProjects={(data: CardItem[]) =>setProjects(data)}/>
  </>);
}