# Deploy steps:
1. Use gcloud cli and cd to TicketTransferApi
2. `docker build -t gcr.io/anthos-dev-9438/tickettransferapi -f Dockerfile .`
3. `docker push gcr.io/anthos-dev-9438/tickettransferapi`
4. Go to gcp to deploy the image