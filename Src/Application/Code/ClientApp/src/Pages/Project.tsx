import { useState, useEffect, Dispatch } from 'react';
import { useParams } from "react-router-dom";
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProject } from '../Interfaces/IPage';
import { ProjectService } from '../Services/ProjectService';
import ProblemBetNewForm from '../Components/ProblemBetNewForm';
import { PageFunctions } from './PageFunctions';
import { ProblemService } from '../Services/ProblemService';

export function Project(pageProps: IPage)
{
    /**
     * GET parameters.
     */
    let { projectId }: { projectId: string } = useParams();

    /**
     * Page model definition.
     */
    var defaultProject: IProject = { name: "", problems: new Array<CardItem>(), isLoaded: false, description: "" };
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

    // Output the page view.
    return <>
        <div className="row">
            <div className="col">
                <h1>{project.name}</h1>
                <p>Once a problem has been added we can then make bets on actions that can fix the issues.</p>
                <h2>Description</h2>
                {project.description}

                <h2>Problems</h2>
                <CardGrid data={project.problems} AddNewClick={(e) => { PageFunctions.DisplayModal(e, dialogOpened, (newValue) => { setDialogOpened(newValue) }) }} />
                <ProblemBetNewForm
                    title="Add Project"
                    description="Use the form to quickly add problems. These can be fleshed out after being created."
                    buttonText="Add Problem"
                    saveAction={(values, setSubmitting, resetForm) =>
                    {
                        ProblemService.Put(projectId, JSON.stringify(values)).then(() =>
                        {
                            ProjectService.Get(projectId).then(
                                (data) =>
                                {
                                    // Sets the model against the page.
                                    setProject(data);

                                    // Resets the add new problem form.
                                    resetForm({});

                                    // Close the dialog.
                                    PageFunctions.CloseDialog('newModal');
                                },
                                (error) =>
                                {
                                    alert(error);
                                }
                            );

                            // We have finished submitting the form.
                            setSubmitting(false);
                        });
                    }} />
            </div>
        </div>
    </>;
}