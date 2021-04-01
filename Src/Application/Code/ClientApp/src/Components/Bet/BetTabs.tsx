import { IBet } from "../../Interfaces/IPage";
import TabComments from "./TabComments";

export default function BetTabs(bet: IBet)
{
    if (bet.isLoaded === false)
        {
            return <></>;
        }

        switch (bet.status)
        {
            case "Created": {
                return <>
                    <nav className="mt-3">
                        <div className="nav nav-tabs" id="nav-tab" role="tablist">
                            <button className="nav-link active" id="nav-comments-tab" data-bs-toggle="tab" data-bs-target="#nav-comments" type="button" role="tab" aria-controls="nav-comments" aria-selected="true">Comments</button>
                        </div>
                    </nav>
                    <div className="tab-content" id="nav-tabContent">
                        <div className="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments-tab">{TabComments()}</div>
                    </div>
                </>
            }
            case "In Progress": {
                return <>
                    <nav className="mt-3">
                        <div className="nav nav-tabs" id="nav-tab" role="tablist">
                            <button className="nav-link active" id="nav-comments-tab" data-bs-toggle="tab" data-bs-target="#nav-comments" type="button" role="tab" aria-controls="nav-comments" aria-selected="true">Comments</button>
                            <button className="nav-link" id="nav-feedback-tab" data-bs-toggle="tab" data-bs-target="#nav-feedback" type="button" role="tab" aria-controls="nav-feedback" aria-selected="false">Feedback</button>
                        </div>
                    </nav>
                    <div className="tab-content" id="nav-tabContent">
                        <div className="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments-tab">{TabComments()}</div>
                        <div className="tab-pane fade" id="nav-feedback" role="tabpanel" aria-labelledby="nav-feedback-tab">...</div>
                    </div>
                </>
            }
            case "Finished": {
                return <>
                    <nav className="mt-3">
                        <div className="nav nav-tabs" id="nav-tab" role="tablist">
                            <button className="nav-link active" id="nav-comments-tab" data-bs-toggle="tab" data-bs-target="#nav-comments" type="button" role="tab" aria-controls="nav-comments" aria-selected="true">Comments</button>
                            <button className="nav-link" id="nav-feedback-tab" data-bs-toggle="tab" data-bs-target="#nav-feedback" type="button" role="tab" aria-controls="nav-feedback" aria-selected="false">Feedback</button>
                            <button className="nav-link" id="nav-outcomes-tab" data-bs-toggle="tab" data-bs-target="#nav-outcomes" type="button" role="tab" aria-controls="nav-outcomes" aria-selected="false">Outcomes</button>
                        </div>
                    </nav>
                    <div className="tab-content" id="nav-tabContent">
                        <div className="tab-pane fade show active" id="nav-comments" role="tabpanel" aria-labelledby="nav-comments-tab">{TabComments()}</div>
                        <div className="tab-pane fade" id="nav-feedback" role="tabpanel" aria-labelledby="nav-feedback-tab">...</div>
                        <div className="tab-pane fade" id="nav-outcomes" role="tabpanel" aria-labelledby="nav-outcomes-tab">...</div>
                    </div>
                </>
            }
        }
}