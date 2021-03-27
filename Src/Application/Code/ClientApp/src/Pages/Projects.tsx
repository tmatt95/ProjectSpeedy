import { useState, useEffect, FormEvent, Dispatch, SetStateAction } from 'react';
import * as bootstrap from 'bootstrap';
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage } from '../Interfaces/IPage';
import { ProjectService } from '../Services/ProjectService'
import ProjectNewForm from '../Components/Projects/ProjectNewForm'

export function Projects(pageProps: IPage)
{
  /**
   * Existing projects.
   */
  const [projects, setProjects]: [CardItem[], Dispatch<SetStateAction<CardItem[]>>] = useState(new Array<CardItem>());

  /**
   * Name of the new project.
   */
  const [newProjectName, setNewProjectName] = useState("");

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
        (data) => { setProjects(data); },
        (error) =>
        {
          alert(error);
        }
      );
    }
  }, [runOnce, pageProps]);

  /**
   * Create New Project
   * @param {*} event The submit form event
   */
  const CreateNewProject = (event: FormEvent<HTMLFormElement>) =>
  {
    event.preventDefault();
    let myModalEl: HTMLElement | null = document.getElementById('newModal');

    if (myModalEl != null)
    {
      let modal: bootstrap.Modal | null = bootstrap.Modal.getInstance(myModalEl);
      modal.hide();
      setNewProjectName("");
      pageProps.globalMessage({ message: "Project added successfully", class: "alert-success" });
    }
  }

  return (<>
    <div className="row">
      <div className="col">
        <h1>Projects</h1>
      </div>
    </div>
    <div>
      <h1>Anywhere in your app!</h1>
    </div>

    <CardGrid data={projects} />

    <ProjectNewForm/>
  </>);
}