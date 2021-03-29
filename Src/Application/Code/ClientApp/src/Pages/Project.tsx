import { useState, useEffect, Dispatch, MouseEvent } from 'react';
import { useParams } from "react-router-dom";
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProject } from '../Interfaces/IPage';
import { ProjectService } from '../Services/ProjectService';
import ProblemNewForm from '../Components/Problem/ProblemNewForm';
import * as bootstrap from 'bootstrap';

export function Project(pageProps: IPage)
{
    /**
     * GET parameters.
     */
    let { projectId }: { projectId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultProject: IProject = { name: "", problems: new Array<CardItem>(), isLoaded: false };
    const [project, setProject]: [IProject, Dispatch<IProject>] = useState(defaultProject);

    /**
     * Used to run code only once on page load.
     */
    const [runOnce, setRunOnce] = useState(false);
    useEffect(() =>
    {
        if (runOnce === false)
        {
            setRunOnce(true);

            // Loads the projects onto the page
            ProjectService.Get(projectId).then(
                (data) =>
                {
                    // Sets the model against the page.
                    setProject(data);
                    data.isLoaded = true;

                    // Sets the project name.
                    document.title = `Project ${project.name}`;

                    // Set the breadcrumbs.
                    pageProps.setBreadCrumbs([
                        { address: "/", text: "Projects", isLast: false },
                        { address: `/project/${projectId}`, text: "Project Name", isLast: true }
                    ]);
                },
                (error) =>
                {
                    alert(error);
                }
            );
        }
    }, [runOnce, pageProps, projectId, project.name]);

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

    // Output the page view.
    return <>
        <div className="row">
            <div className="col">
                <h1>{project.name}</h1>
                <p>Once a problem has been added we can then make bets on actions that can fix the issues.</p>
            </div>
        </div>

        <CardGrid data={project.problems} AddNewClick={(e) => {DisplayModal(e)}} />
        <ProblemNewForm projectId={projectId} setProject={(data: IProject) => { setProject(data);}}/>
    </>;
}