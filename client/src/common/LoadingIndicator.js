import React , {CSSProperties}from "react";
import { usePromiseTracker } from "react-promise-tracker";
import ClipLoader from "react-spinners/ClipLoader";



export default function LoadingIndicator(props) {
  const { promiseInProgress } = usePromiseTracker();
  const cssOverride={
    backgroundColor: '#404258'
  }
  return (
    promiseInProgress &&
    <ClipLoader className="loading-indicator" color= "#de6864" loading={promiseInProgress} cssOverride={cssOverride}/>
  );
}
