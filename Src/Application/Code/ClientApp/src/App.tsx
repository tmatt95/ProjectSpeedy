import { Dispatch, useState } from 'react';
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
import { Bet } from './Pages/Bet';
import { IPage } from './Interfaces/IPage';

export default function App()
{
  /**
   * Show / hide / update the global message at the top of the page.
   * @param param0 
   * @returns 
   */
  function GlobalMessage({message}: { message: IGlobalMessage })
  {
    if (message !== null)
    {
      let classes = `alert alert-dismissible fade show ${message.class}`;
      return <>
        <div className={classes} role="alert">
          {message.message}
          <button type="button" className="btn-close" onClick={() => setGlobalMessage({ message: "", class: "d-none" })} aria-label="Close"></button>
        </div>
      </>;
    }
    else
    {
      return <></>
    }
  }

  // Used to store page messages.
  const [globalMessage, setGlobalMessage]: [IGlobalMessage, Dispatch<IGlobalMessage>] = useState({ message: "", class: "d-none" });

  // Used to store the data used to generate the breadcrumbs for the page.
  var initalCrumbs: BreadCrumbItem[] = [{ text: "Projects", address: "/", isLast: true }];
  const [breadCrumbs, setBreadCrumbs]: [BreadCrumbItem[], Dispatch<BreadCrumbItem[]>] = useState(initalCrumbs);

  var pageProps: IPage = {
    breadCrumbs: breadCrumbs,
    setBreadCrumbs: (crumbs: BreadCrumbItem[]) => { setBreadCrumbs(crumbs) },
    globalMessage: (alertMessage: IGlobalMessage) => setGlobalMessage(alertMessage),
  }

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
              <GlobalMessage message={globalMessage} />
            </div>
          </div>
        </div>

        <div className="container">
          <Switch>
            <Route path="/project">
              <ProjectSection {...pageProps} />
            </Route>
            <Route path="/">
              <Projects {...pageProps} />
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
function ProjectSection(pageProps: IPage)
{
  let match = useRouteMatch();
  return (
    <>
      <Switch>
        <Route path={`${match.path}/:projectId/:problemId/:betId`}>
          <Bet {...pageProps} />
        </Route>
        <Route path={`${match.path}/:projectId/:problemId`}>
          <Problem {...pageProps} />
        </Route>
        <Route path={`${match.path}/:projectId`}>
          <Project {...pageProps} />
        </Route>
        <Route path={match.path}>
          <h3>Project Id not supplied</h3>
        </Route>
      </Switch>
    </>
  );
}