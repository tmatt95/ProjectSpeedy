import { useState, useEffect, Dispatch } from 'react';
import { useParams } from "react-router-dom";
import { CardGrid, CardItem } from '../Components/CardGrid'
import { IPage, IProject } from '../Interfaces/IPage';
import { ProjectService } from '../Services/ProjectService';
import ProblemNewForm from '../Components/Problem/ProblemNewForm';

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
                    pageProps.setBreadCrumbs([]);
                    pageProps.setBreadCrumbs(pageProps.breadCrumbs.concat([{ address: "/", text: "Projects", isLast: false }]));
                    pageProps.setBreadCrumbs(pageProps.breadCrumbs.concat([{ address: "", text: data.name, isLast: true }]));
                },
                (error) =>
                {
                    alert(error);
                }
            );
        }
    }, [runOnce, pageProps, projectId, project.name]);

    // Output the page view.
    return <>
        <div className="row">
            <div className="col">
                <h1>{project.name}</h1>
                <p>Once a problem has been added we can then make bets on actions that can fix the issues.</p>
            </div>
        </div>

        <CardGrid data={project.problems} />
        <ProblemNewForm projectId={projectId} setProject={(data: IProject) => { setProject(data);}}/>
    </>;
}