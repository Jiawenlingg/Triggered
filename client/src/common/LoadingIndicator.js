import React , {CSSProperties}from "react";
import { usePromiseTracker } from "react-promise-tracker";
import ClipLoader from "react-spinners/ClipLoader";
import './LoadingIndicator.css'


export default function LoadingIndicator(props) {
  const { promiseInProgress } = usePromiseTracker();
  const cssOverride={
    backgroundColor: '#404258'
  }
  return (
    <div className="loading-indicator">
      {promiseInProgress &&
      <ClipLoader color= "#de6864" loading={promiseInProgress} cssOverride={cssOverride}/>}
    </div>
    );
}
