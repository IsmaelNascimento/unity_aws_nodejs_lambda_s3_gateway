const AWS = require('aws-sdk');
const s3 = new AWS.S3();
const BUCKET_NAME = '';

exports.handler = (event, context, callback) => {
     let base64data = Buffer.from(event.base64, 'base64');
     
     let params = {
      Bucket: BUCKET_NAME,
      Key: event.pathFile,
      Body: base64data,
      ACL: 'public-read'
    };
      
      s3.upload(params, function (err, data) {
        if (err) {
          console.log('error in callback');
          console.log(err);
          callback(err, null);
        }
        console.log('success');
        console.log(data);
        callback(null, data);
      });
};