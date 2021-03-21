import React, { Dispatch, useState } from 'react';
import './App.css';
import { Projects } from './Pages/Projects';
import { Project } from './Pages/Project';
import { Problem } from './Pages/Problem';
import { IGlobalMessage } from './Interfaces/Components';
import { BreadCrumbItem, BreadCrumbs } from './Components/BreadCrumbs';
import
  {
    BrowserRouter as Router,
    Switch,
    Route,
    Link,
    useRouteMatch,
  } from "react-router-dom";


/**
 * Show / hide / update the global message at the top of the page.
 * @param param0 
 * @returns 
 */
function GlobalMessage({ globalMessage, message }: { globalMessage: (arg0: IGlobalMessage) => void, message: IGlobalMessage })
{
  if (message !== null)
  {
    let classes = `alert alert-dismissible fade show ${message.class}`;
    return <>
      <div className={classes} role="alert">
        {message.message}
        <button type="button" className="btn-close" onClick={() => globalMessage({ message: "", class: "d-none" })} aria-label="Close"></button>
      </div>
    </>;
  }
  else
  {
    return <></>
  }
}

export default function App()
{
  // Used to store page messages.
  const [globalMessage, setGlobalMessage]: [IGlobalMessage, Dispatch<IGlobalMessage>] = useState({ message: "", class: "d-none" });

  // Used to store the data used to generate the breadcrumbs for the page.
  var initalCrumbs: BreadCrumbItem[] = [{ text: "Projects", address: "/", isLast: true }];
  const [breadCrumbs, setBreadCrumbs]: [BreadCrumbItem[], Dispatch<BreadCrumbItem[]>] = useState(initalCrumbs);

  return (
    <>
      <Router>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <div className="container">
            <span className="navbar-brand mb-0 h1">Project Speedy</span>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav">
                <li className="nav-item">
                  <Link className="nav-link" to="/">Projects</Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>

        <div className="container">
          <div className="row mt-2">
            <div className="col">
              <BreadCrumbs breadCrumbs={breadCrumbs} />
            </div>
          </div>
        </div>

        <div className="container">
          <div className="row">
            <div className="col">
              <GlobalMessage globalMessage={(alertMessage) => setGlobalMessage(alertMessage)} message={globalMessage} />
            </div>
          </div>
        </div>

        <div className="container">
          <Switch>
            <Route path="/project">
              <ProjectSection breadCrumbs={breadCrumbs} setBreadCrumbs={(crumbs: BreadCrumbItem[]) => { setBreadCrumbs(crumbs) }} globalMessage={(alertMessage: IGlobalMessage) => setGlobalMessage(alertMessage)} />
            </Route>
            <Route path="/">
              <Projects breadCrumbs={breadCrumbs} setBreadCrumbs={(crumbs: BreadCrumbItem[]) => { setBreadCrumbs(crumbs) }} globalMessage={(alertMessage: IGlobalMessage) => setGlobalMessage(alertMessage)} />
            </Route>
          </Switch>
        </div>
      </Router>
    </>
  );
}

/**
 * Contains links to the project section of the application.
 * @param {*} props Properties sent into the section.
 */
function ProjectSection({ setBreadCrumbs, breadCrumbs, globalMessage }: { setBreadCrumbs: (crumbs: BreadCrumbItem[]) => void, breadCrumbs: BreadCrumbItem[], globalMessage: (alertMessage: IGlobalMessage) => void })
{
  let match = useRouteMatch();
  return (
    <>
      <Switch>
        <Route path={`${match.path}/:projectId/:problemId/:betId`}>
          <h1>Bet</h1>
        </Route>
        <Route path={`${match.path}/:projectId/:problemId`}>
          <Problem breadCrumbs={breadCrumbs} setBreadCrumbs={(crumbs: BreadCrumbItem[]) => { setBreadCrumbs(crumbs) }} globalMessage={globalMessage} />
        </Route>
        <Route path={`${match.path}/:projectId`}>
          <Project breadCrumbs={breadCrumbs} setBreadCrumbs={(crumbs: BreadCrumbItem[]) => { setBreadCrumbs(crumbs) }} globalMessage={globalMessage} />
        </Route>
        <Route path={match.path}>
          <h3>Project Id not supplied</h3>
        </Route>
      </Switch>
    </>
  );
}